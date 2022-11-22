import * as React from "react";
import { useEffect, useState } from "react";
import { ServiceErrorCarrier } from "foundation.contracts";
import { ITodoItemReader, TodoItem } from "todos.models"
import { TodoItemView } from "./TodoItemView";

export const TodoItemListView = (props: {
  reader: ITodoItemReader
}) => {
  const [data, setData] = useState<TodoItem[] | ServiceErrorCarrier | null>(null);
  const fetch = async () => { setData(await props.reader.getAllItems()); }
  useEffect(() => { fetch(); }, []);

  if (data == null) {
    return <p></p>;
  }
  else if ("error" in data) {
    const errorCarrier = data as ServiceErrorCarrier;
    const error = errorCarrier.error;
    return <p>{error.message}</p>;
  }
  else {
    const items = data as TodoItem[];
    return <ul>
      {items.map(item => <TodoItemView item={item} key={item.id} />)}
    </ul>;
  }
};
