import { Component, OnInit } from '@angular/core';
import { EmployeeApiService } from '../../services/employee-api.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss'],
})
export class EmployeeListComponent implements OnInit {
  displayedColumns: string[] = ['ID', 'FIRST', 'LAST', 'EMPLOYEE_STATUS'];
  constructor(public employeeApiService: EmployeeApiService) {}

  ngOnInit(): void {}
}
