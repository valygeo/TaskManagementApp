import { Sprint } from './Sprint';

export class Project {
  project_Id: number;
  project_Name: string;
  project_Description: string;
  start_Date_Project: Date;
  end_Date_Project: Date;
  isDeleted: number;
  sprints: Sprint[];
}
