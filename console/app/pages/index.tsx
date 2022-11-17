import { ITodoItemReader } from "todos.models";
import { TodoItemListView } from "todos.views";
import { AxiosTodoItemReader } from "todos.http";
import axios from "axios";

axios.defaults.baseURL = "https://localhost:7180/"; 

export default function App() {
  return (
    <div>
      <TodoItemListView reader={new AxiosTodoItemReader(axios)} />
    </div>
  );
}
