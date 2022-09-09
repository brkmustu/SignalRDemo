import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";

class carHub {
  constructor() {
    this.client = new HubConnectionBuilder()
      .withUrl("https://localhost:7000/hubs/car-hubs", {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();
  }
  start() {
    this.client.start();
  }
}

export const getCarHub = () => {
  return new carHub();
};
