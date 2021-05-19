import { IBaseModel } from 'src/app/core/models/i-base.model';
import { EmployeeStatuses } from './employee-statuses.enum';

export interface IEmployeeModel extends IBaseModel {
  FIRST_NAME: string;
  LAST_NAME: string;
  EMPLOYEE_STATUS: EmployeeStatuses;
}
