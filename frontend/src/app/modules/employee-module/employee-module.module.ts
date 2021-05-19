import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeApiService } from './services/employee-api.service';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeRoutingModule } from './employee-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { EmployeeStatusStringPipe } from './pipes/employee-status-string.pipe';

const MAT_MODULES = [MatPaginatorModule, MatSortModule, MatTableModule];

@NgModule({
  providers: [EmployeeApiService],
  declarations: [EmployeeListComponent, EmployeeStatusStringPipe],
  imports: [
    CommonModule,
    ...MAT_MODULES,
    EmployeeRoutingModule,
    HttpClientModule,
  ],
})
export class EmployeeModule {}
