import axios from "axios";
export const appAxios = axios.create({
  baseURL: "https://localhost:7000",
  withCredentials: false,
  headers: {
    "Content-Type": "application/json",
  },
});

export const getCars = (fnThen) => {
  appAxios
    .get("/cars/getall")
    .then(fnThen)
    .catch(function (error) {
      console.log(error);
      return error;
    });
};

export const getOrders = (fnThen) => {
  appAxios
    .get("/orders/getall")
    .then(fnThen)
    .catch(function (error) {
      console.log(error);
      return error;
    });
};
