import {InvoiceItems} from "./invoice-items";
import {Item} from "./item";

export interface Jobs {
  id: number;
  address: string;
  city: string;
  processing_Status: string;
  createdDateTime: string;
  finishedDateTime?: string;
  scheduledStartDateTime: string;
  scheduledEndDateTime: string;
  assigned_Technician_Id: string;
  assigned_Customer_Id: string;
  totalPrice: number;
  invoiceNumber: string;
  invoiceItems: Item[];
  invoiceId: string;
  companyId: string

}
