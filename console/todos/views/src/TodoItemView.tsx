import * as React from "react";
import { TodoItem } from "todos.models";

export function TodoItemView(props: { item: TodoItem }): JSX.Element {
  const item = props.item;
  return <li>
    <p>{item.text}</p>
  </li>;
}
