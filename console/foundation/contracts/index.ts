export class ServiceError {
  code: string;
  message: string;

  constructor(code: string, message: string) {
    this.code = code;
    this.message = message;
  }
};

export class ArrayCarrier<T> {
  items: T[];
  
  constructor(items: T[]) {
    this.items = items;
  }
};
