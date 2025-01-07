import { Pbi } from './Pbi';
import { Project } from './Project';
import { Task } from './Task';

export class User {
  id: number;
  username: string;
  email: string;
  password: string;
  isDeleted: number;
  projects: Project[];
  tasks: Task[];
  pbi: Pbi[];
}
