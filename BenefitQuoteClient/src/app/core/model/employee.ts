import {IEmployee} from './IEmployee';

export class Employee implements IEmployee {
  id: string;
  name: string;

  constructor(id: string, name: string) {
    this.id = id;
    this.name = name;
  }
}

