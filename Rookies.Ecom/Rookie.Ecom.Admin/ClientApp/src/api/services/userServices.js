import EndPoints from "../endpoints";
import { get } from "../httpHelper";

export const getUserRequest = () => {
  return get(`${EndPoints.Users}/GetAll`);
};
