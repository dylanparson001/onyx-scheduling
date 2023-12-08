import { Item } from "./item";

export interface Invoice {
  id: number,
  address: string,
  invoiceNumber: string,
  assigned_Customer_Id: number,
  assigned_Technician_Id: number,
  total_Price: number,
  items: Item[],
  createdDateTime: Date,
  finishedDateTime: Date,
  invoiceInvoice_Items: string
}
