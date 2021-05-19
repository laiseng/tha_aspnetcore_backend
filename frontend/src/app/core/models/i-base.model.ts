import { Statuses } from './statuses.enum';
export interface IBaseModel {
  ID: string; //int
  STATUS: Statuses; //Statuses
  CREATEBY: number; //int
  EDITBY: number; //int
  CREATEDATE: Date;
  EDITDATE: Date;
}
