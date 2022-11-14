import { ITodoItemReader } from "todos.models";
import { TodoItemListView } from "todos.views";
import { AxiosTodoItemReader } from "todos.http";
import axios from "axios";

axios.defaults.baseURL = "https://localhost:7180/"; 
const reader: ITodoItemReader = new AxiosTodoItemReader(axios);

export default function App() {
  return (
    <div>
      <TodoItemListView reader={reader} />
    </div>
  );
}
