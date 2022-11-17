import { Axios } from "axios";
import { ArrayCarrier, ServiceErrorCarrier } from "foundation.contracts";
import { ITodoItemReader, TodoItem } from "todos.models";

export default class AxiosTodoItemReader implements ITodoItemReader {
  axios: Axios;

  constructor(axios: Axios) {
    this.axios = axios;
  }

  async getAllItems(): Promise<TodoItem[] | ServiceErrorCarrier> {
    const response = await this.axios.get<ArrayCarrier<TodoItem>>("api/todos");
    return response.data.items;
  }
};
