import {User} from "./user";
import {Invoice} from "./invoice";

export interface invoiceCustomer {
  customer: User,
  invoice: Invoice,
  companyId: string


}
