import { Pipe, PipeTransform } from '@angular/core';
import { EmployeeStatuses } from '../models/employee-statuses.enum';

@Pipe({
  name: 'statusesString',
})
export class EmployeeStatusStringPipe implements PipeTransform {
  transform(value: EmployeeStatuses): string {
    return EmployeeStatuses[value];
  }
}
