import { ServiceError } from "./ServiceError";

export class ServiceErrorCarrier {
  constructor(public error: ServiceError) {
  }
}
