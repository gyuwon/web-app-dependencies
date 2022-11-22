import { ServiceErrorCarrier } from "foundation.contracts";
import TodoItem from "./TodoItem";

export default interface ITodoItemReader {
  getAllItems(): Promise<TodoItem[] | ServiceErrorCarrier>;
}
