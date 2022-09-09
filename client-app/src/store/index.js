import { createStore } from "vuex";

const store = createStore({
  state: {
    cars: [],
    carImages: [],
    selectedCar: {
      id: 1,
      code: "",
      name: "",
      imageUrl: "",
      description: "",
    },
    signInInfo: {
      authenticated: false,
      user: {
        email: "",
        roles: [],
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
    carImages: (state) => (carId) =>  {
      return state.carImages.filter(x => x.carId == carId);
    },
    user(state) {
      return state.signInInfo.user;
    },
    authenticated(state) {
      return state.signInInfo.authenticated;
    },
    userRoles(state) {
      return state.signInInfo ? state.signInInfo.user.roles : [];
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
      state.cars = [];
      cars.map((x) => state.cars.push(x));
      state.selectedCar = cars.find(x => x.id == 1);
    },
    setCarImages(state, carImages) {
      state.carImages = [];
      carImages.map((x) => state.carImages.push(x));
    },
    setSelectedCar(state, carCode) {
      state.selectedCar = state.cars.find((x) => x.code == carCode);
    },
    setCarImageUrl(state, parameterObj) {
      console.log(state, parameterObj);
      const car = state.cars.find((x) => x.id == parameterObj.id);
      const newCars = state.cars.filter((x) => x.id != parameterObj.id);
      car.imageUrl = parameterObj.imageUrl;
      newCars.push(car);
      state.cars = newCars;
    },
  },
});

export default store;
