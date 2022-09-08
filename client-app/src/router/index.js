import { createRouter, createWebHistory } from "vue-router";
import PresentationView from "@/views/Presentation/PresentationView.vue";
import PresentationListView from "@/views/Presentation/PresentationListView.vue";
import SignInView from "@/views/LandingPages/SignIn/SignInView.vue";
import RegisterView from "@/views/LandingPages/Register/RegisterView.vue";
import store from "@/store/index.js";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "presentation",
      component: PresentationView,
      meta: {
        permissions: ["user"],
      },
    },
    {
      path: "/list",
      name: "presentation-list",
      component: PresentationListView,
      meta: {
        permissions: ["user"],
      },
    },
    {
      path: "/signin",
      name: "signin",
      component: SignInView,
    },
    {
      path: "/register",
      name: "register",
      component: RegisterView,
    },
  ],
});

router.beforeEach((to, from, next) => {
  const userRoles = store.getters.userRoles;

  if (to.meta.permissions && to.meta.permissions.length > 0) {
    let isAllowed = userRoles.some((p) => to.meta.permissions.includes(p));

    if (!isAllowed) next("/signin");
    else if (["signin", "register"].indexOf(to.name) > 0) next("/");
  }

  next();
});

export default router;
