import AboutUsPage from "../pages/AboutUsPage.vue";
import ContactUsPage from "../pages/ContactUsPage.vue";
import HomePage from "../pages/HomePage.vue";
import LoginPage from "../pages/LoginPage.vue";
import MyAccountPage from "../pages/MyAccountPage.vue";
import ProductDetailsPage from "../pages/ProductDetailsPage.vue";
import ProductsPage from "../pages/ProductsPage.vue";
import ShoppingCartPage from "../pages/ShoppingCartPage.vue";
import SignupPage from "../pages/SignUpPage.vue";

const routes = [
    { path: "/", name: "Home", component: HomePage },
    { path: "/products", name: "Products", component: ProductsPage },
    { path: "/product-details", name: "ProductDetails", component: ProductDetailsPage },
    { path: "/about-us", name: "AboutUs", component: AboutUsPage },
    { path: "/contact-us", name: "ContactUs", component: ContactUsPage },
    { path: "/account", name: "MyAccount", component: MyAccountPage },
    { path: "/cart", name: "ShoppingCart", component: ShoppingCartPage },
    { path: "/login", name: "Login", component: LoginPage },
    { path: "/signup", name: "Signup", component: SignupPage },
    { path: "/:pathMatch(.*)*", name: "not-found", component: () => import("../pages/NotFoundPage.vue") },
];

export default routes;
