import {InvoiceItems} from "./invoice-items";

export interface Jobs {
  id: number;
  address: string;
  city: string;
  processingStatus: string;
  createdDateTime: string;
  finishedDateTime?: string;
  scheduledStartDateTime: string;
  scheduledEndDateTime: string;
  assignedTechnicianId: string;
  assignedCustomerId: string;
  totalPrice: number;
  invoiceNumber: number;
  invoiceItems: InvoiceItems[];
  invoiceId: number;
}
