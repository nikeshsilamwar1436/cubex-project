import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [{ path: '', component: () => import('pages/IndexPage.vue') },
      { path: '/orders', component: () => import('pages/OrdersPage.vue') },
      { path: '/products', component: () => import('pages/ProductsPage.vue') },
      { path: '/categories', component: () => import('pages/CategoriesPage.vue') }
    ],

  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue'),
  },
];

export default routes;
