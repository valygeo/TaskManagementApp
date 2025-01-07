import { Type } from '@angular/compiler';

export class PbiDto {
  pbi_Name: string;
  pbi_Description: string;
  start_Date_Pbi: Date;
  end_Date_Pbi: Date;
  is_Deleted: number;
  pbi_type: PbiType;
  id: number;
  sprint_Id: number;
  tasks: Task[];
}
export enum PbiType {
  Bug,
  Feature,
}
