import { Type } from '@angular/compiler';

export class Pbi {
  pbi_Id: number;
  pbi_Name: string;
  pbi_Description: string;
  start_Date_Pbi: Date;
  end_Date_Pbi: Date;
  is_Deleted: number;
  id: number;
  pbi_type: PbiType;
  sprint_Id: number;
  tasks: Task[];
}
export enum PbiType {
  Bug,
  Feature,
}
