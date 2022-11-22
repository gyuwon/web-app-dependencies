import React from "react";
import { describe, expect, test } from "@jest/globals";
import { ServiceError, ServiceErrorCarrier } from "foundation.contracts";
import { ITodoItemReader, TodoItem } from "todos.models";
import { v4 as uuidv4 } from "uuid";
import { TodoItemListView } from "./index";
import { render, screen, within } from "@testing-library/react";

class TodoItemReaderStub implements ITodoItemReader {
  items: TodoItem[] = [];

  getAllItems(): Promise<TodoItem[] | ServiceErrorCarrier> {
    return Promise.resolve(this.items);
  }
};

class Calls {
  getAllItems: [][] = []
}

class TodoItemReaderSpy implements ITodoItemReader {
  calls = new Calls();

  getAllItems(): Promise<TodoItem[] | ServiceErrorCarrier> {
    this.calls.getAllItems.push([]);
    return Promise.resolve([]);
  }
};

class BrokenTodoItemReader implements ITodoItemReader {
  public errorMessage: string = uuidv4();

  getAllItems(): Promise<TodoItem[] | ServiceErrorCarrier> {
    const error = new ServiceError("Erorr", this.errorMessage);
    return Promise.resolve(new ServiceErrorCarrier(error));
  }
}

describe("Given single todo item", () => {
  // Arrange
  const item = new TodoItem(uuidv4(), uuidv4(), false);
  const stub = new TodoItemReaderStub();
  stub.items.push(item);

  test("sut renders list", async () => {
    // Act
    render(<TodoItemListView reader={stub} />);

    // Assert
    const list: HTMLElement = await screen.findByRole("list");
    expect(list).toBeTruthy();
  });

  test("sut renders list item", async () => {
    // Act
    render(<TodoItemListView reader={stub} />);

    // Assert
    const listItems: HTMLElement[] = await getListItems();
    expect(listItems.length).toBe(1);
  });

  test("sut correctly renders text of todo item", async () => {
    // Act
    render(<TodoItemListView reader={stub} />);

    // Assert
    const listItem = await getSingleListItem();
    expect(listItem.textContent).toContain(item.text);
  });
});

test("Sut reads todo items once", async () => {
  // Arrange
  const spy = new TodoItemReaderSpy();

  // Act
  render(<TodoItemListView reader={spy} />);

  // Assert
  await screen.findByRole("list");
  expect(spy.calls.getAllItems).toHaveLength(1);
});

describe("Given reader fails", () => {
  test("sut shows error message", async () => {
    const stub = new BrokenTodoItemReader();
    render(<TodoItemListView reader={stub} />);
    expect(await screen.findByText(stub.errorMessage)).toBeTruthy();
  });
});

async function getListItems(): Promise<HTMLElement[]> {
  const { getAllByRole } = within(await screen.findByRole("list"));
  return getAllByRole("listitem");
}

async function getSingleListItem(): Promise<HTMLElement> {
  const listItems: HTMLElement[] = await getListItems();
  return listItems[0];
}
