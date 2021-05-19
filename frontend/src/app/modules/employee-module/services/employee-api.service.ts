import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IdmArrayState } from 'src/app/core/states/idm-array-state';
import { environment } from 'src/environments/environment';
import { IEmployeeModel } from '../models/i-employee.model';
import { of } from 'rxjs';

@Injectable()
export class EmployeeApiService {
  employeeList: IdmArrayState<IEmployeeModel>;
  constructor(private httpClient: HttpClient) {
    this.employeeList = new IdmArrayState<IEmployeeModel>(this.getList());
  }

  getList() {
    return this.httpClient.get<IEmployeeModel[]>(
      `${environment.ApiHost}employee/all`
    );
  }
}
