import { ReplaySubject, Subscription } from 'rxjs';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { switchMap, tap } from 'rxjs/operators';
import { SourceStates } from './source-states.type';

export abstract class IdmAbstractStateBase<T> {
  private subscription!: Subscription;
  store!: T;
  private storeSubject!: ReplaySubject<T>;
  store$: Observable<T>;
  private sourceStateSubject = new BehaviorSubject<SourceStates>('initial');
  sourceState$ = this.sourceStateSubject.asObservable();
  private source$!: Observable<T>;
  constructor(source$: Observable<T>) {
    this.source$ = source$;
    this.storeSubject = new ReplaySubject<T>(1);
    this.store$ = this.storeSubject.asObservable().pipe(
      switchMap((nextValue) => {
        if (nextValue) {
          this.store = nextValue;
          return of(nextValue);
        } else {
          return this.source$.pipe(
            tap((sourceVal) => {
              this.store = sourceVal;
            })
          );
        }
      })
    );

    this.loadSource();
  }

  updateStore(val: T): void {
    this.storeSubject.next(val);
  }

  refresh(): void {
    this.loadSource();
  }

  loadSource(): void {
    this.sourceStateSubject.next('loading');
    this.subscription = this.source$.subscribe(
      (sourceVal) => {
        this.store = sourceVal;
        this.sourceStateSubject.next('completed');
        this.storeSubject.next(sourceVal);
      },
      (error) => {
        this.sourceStateSubject.next('error');
      }
    );
  }
}
