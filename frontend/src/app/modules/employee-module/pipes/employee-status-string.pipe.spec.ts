import { EmployeeStatusStringPipe } from './employee-status-string.pipe';

describe('EmployeeStatusStringPipe', () => {
  it('create an instance', () => {
    const pipe = new EmployeeStatusStringPipe();
    expect(pipe).toBeTruthy();
  });
});
