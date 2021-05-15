import { HealthStatus } from "./HealthStatus";
import { ContractorType } from "./ContractorType";

export interface Contractor {
  id: number;
  name: string;
  address: string;
  phoneNumber: string;
  contractorType: ContractorType,
  healthStatus: HealthStatus,
}
