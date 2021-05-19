import { BehaviorSubject, Observable, of } from 'rxjs';
import { IdmAbstractStateBase } from './idm-abstract-state-base';

export class IdmArrayState<T> extends IdmAbstractStateBase<Array<T>> {
  updateStoreArrayItem(item: T, updatedItem: T) {
    let index = this.store.indexOf(item);
    if (index > -1) {
      this.store[index] = Object.assign(item, updatedItem);
      this.updateStore(this.store);
    }
  }
}
