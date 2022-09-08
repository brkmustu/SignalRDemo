import axios from "axios";
export const appAxios = axios.create({
  baseURL: "http://localhost:5000",
  withCredentials: false,
  headers: {
    "Content-Type": "application/json",
  },
});

export const setJwtTokenHeader = (jwtToken) => {
  appAxios.defaults.headers.common["Authorization"] = "Bearer " + jwtToken;
};

export const cleanTokenHeader = () => {
  appAxios.defaults.headers.common["Authorization"] = "";
};

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
