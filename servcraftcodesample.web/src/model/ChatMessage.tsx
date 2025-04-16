export class ChatMessage {
  public message: string;
  public time: Date;
  public isBot: boolean;

  constructor(message: string, time: Date, isBot: boolean) {
    this.message = message;
    this.time = time;
    this.isBot = isBot;
  }
}