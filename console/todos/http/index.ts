import { ArrayCarrier, ServiceError } from "foundation.contracts";
import { ITodoItemReader, TodoItem } from "todos.models";
import { Axios } from "axios";

export class AxiosTodoItemReader implements ITodoItemReader {
  axios: Axios;

  constructor(axios: Axios) {
    this.axios = axios;
  }

  getAllItems(): Promise<ArrayCarrier<TodoItem> | ServiceError> {
    return Promise.resolve(new ArrayCarrier<TodoItem>([]));
  }
};
