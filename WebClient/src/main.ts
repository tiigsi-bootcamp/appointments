import { createApp } from 'vue';
import { createRouter, createWebHistory } from 'vue-router';

import { TokenService } from './services/token-service';

import App from './App.vue';
import Login from './pages/Login.vue';
import Doctors from './pages/Doctors.vue';
import Home from './pages/Home.vue';
import Contact from './pages/Contact.vue'
import Signup from './pages/Signup.vue';
import one from './pages/booking1.vue';
import two from './pages/booking2.vue';
import Signout from './pages/Signout.vue';

import './style.css';

const router = createRouter({
  routes: [
    { path: '/', component: Home },
    { path: '/login', component: Login },
    { path: '/doctors', component: Doctors },
    { path: '/contact', component: Contact },
    { path: '/signup', component: Signup },
    { path: '/signout', component: Signout },
    { path: '/one', component: one },
    { path: '/two', component: two },
  ],
  history: createWebHistory(),
});

router.beforeEach((to) => {
  if (to.path === '/login' || to.path === '/') {
    return true;
  }

  if (TokenService.isLoggedIn.value) {
    return true;
  }
  
  return { path: '/login', query: { returnUrl: to.path } };
});

const app = createApp(App);

// Tell the application to use the router.
app.use(router);

app.mount('#app');
