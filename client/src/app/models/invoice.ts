import {Item} from "./item";

export interface Invoice {
  id: number,
  address: string,
  city: string,
  invoiceNumber: string,
  assigned_Customer_Id: string,
  assigned_Technician_Id: string,
  total_Price: number,
  invoiceInvoice_Items: Item[],
  processing_Status: string,
  createdDateTime: string,
  finishedDateTime: string,
  scheduledStartDateTime: string
  scheduledEndDateTime: string
}
