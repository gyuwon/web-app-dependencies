import { describe, expect, test } from "@jest/globals";
import { v4 as uuidv4 } from "uuid";
import { AxiosTodoItemReader } from "todos.http";
import { TodoItem } from "todos.models";
import { ArrayCarrier } from "foundation.contracts";
import axios from "axios";
import MockAxios from "jest-mock-axios";

describe("getAllItems",() =>{
  test("correctly call get method", async () => {
    // Arrange
    MockAxios.get.mockResolvedValueOnce({ data: new ArrayCarrier([]) });
    const sut = new AxiosTodoItemReader(axios);
  
    // Act
    await sut.getAllItems();
  
    // Assert
    expect(axios.get).toBeCalledTimes(1);
    expect(axios.get).toHaveBeenCalledWith("api/todos");
  });
  
  test("correctly returns response data", async () => {
    const items = [
      new TodoItem(uuidv4(), uuidv4(), false),
      new TodoItem(uuidv4(), uuidv4(), false),
      new TodoItem(uuidv4(), uuidv4(), false),
    ];
    // Arrange
    MockAxios.get.mockResolvedValueOnce({ data: new ArrayCarrier(items) });
    const sut = new AxiosTodoItemReader(axios);
  
    // Act
    const actual = await sut.getAllItems();
  
    // Assert
    expect(actual as TodoItem[]).toEqual(items);
  });  
});
