import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { BalanceAndOperations } from "./components/BalanceAndOperations";
import { Game } from "./components/Game";

const AppRoutes = [
  {
    index: true,
    element: <Game />
  },
  {
    path: '/balance',
    requireAuth: true,
    element: <BalanceAndOperations />
  },
  ...ApiAuthorzationRoutes
];

export default AppRoutes;
