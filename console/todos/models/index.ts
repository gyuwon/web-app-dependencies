import { ArrayCarrier, ServiceError } from "foundation.contracts";

export class TodoItem {
  id: string;
  text: string;
  isDone: boolean;
  
  constructor(id: string, text: string, isDone: boolean) {
    this.id = id;
    this.text = text;
    this.isDone = isDone;
  }
}

export interface ITodoItemReader {
  getAllItems(): Promise<ArrayCarrier<TodoItem> | ServiceError>
};
