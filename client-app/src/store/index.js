import { createStore } from "vuex";

const store = createStore({
  state: {
    cars: [],
    selectedCar: {
      id: 1,
      code: "",
      name: "",
      imageUrl: "",
      description:""
    },
    signInInfo: {
      authenticated: false,
      user: {
        id: 0,
        email: "",
        permissions: [],
      },
      jwtToken: "",
    },
  },
  getters: {
    selectedCar(state) {
      return state.selectedCar;
    },
    cars(state) {
      return state.cars;
    },
    user(state) {
      return state.signInInfo.user;
    },
    authenticated(state) {
      return state.signInInfo.authenticated;
    },
    userPermissions(state) {
      return state.signInInfo ? state.signInInfo.user.permissions : [];
    },
  },
  mutations: {
    setSignOut(state) {
      state.signInInfo = {
        authenticated: false,
        user: {
          id: 0,
          email: "",
          permissions: [],
        },
        jwtToken: "",
      };
    },
    setSignInInfo(state, parameterObj) {
      state.signInInfo = {
        authenticated: true,
        user: parameterObj.user,
        jwtToken: parameterObj.jwtToken,
      };
    },
    setCars(state, cars) {
      cars.map(x => state.cars.push(x))
      state.selectedCar = cars[0];
    },
    setSelectedCar(state, carCode) {
      state.selectedCar = state.cars.find((x) => x.code == carCode);
    },
    setCarImageUrl(state, parameterObj) {
      console.log(state, parameterObj);
      const car = state.cars.find((x) => x.code == parameterObj.carCode);
      const newCars = state.cars.filter((x) => x.code != parameterObj.carCode);
      car.imageUrl = parameterObj.imageUrl;
      newCars.push(car);
      state.cars = newCars;
    },
  },
});

export default store;
