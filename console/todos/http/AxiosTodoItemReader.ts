import { Axios } from "axios";
import { ServiceErrorCarrier } from "foundation.contracts";
import { ITodoItemReader, TodoItem } from "todos.models";

export default class AxiosTodoItemReader implements ITodoItemReader {
  axios: Axios;

  constructor(axios: Axios) {
    this.axios = axios;
  }

  getAllItems(): Promise<TodoItem[] | ServiceErrorCarrier> {
    return Promise.resolve([]);
  }
};
