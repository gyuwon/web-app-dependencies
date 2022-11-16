export class ServiceError {
  code: string;
  message: string;

  constructor(code: string, message: string) {
    this.code = code;
    this.message = message;
  }
};

export class ServiceErrorCarrier {
  error: ServiceError;

  constructor(error: ServiceError) {
    this.error = error;
  }
};

export class ArrayCarrier<T> {
  items: T[];
  
  constructor(items: T[]) {
    this.items = items;
  }
};
