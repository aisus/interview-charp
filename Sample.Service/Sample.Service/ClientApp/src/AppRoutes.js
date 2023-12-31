import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { BalanceAndOperations } from "./components/BalanceAndOperations";
import { Game } from "./components/Game";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/game',
    requireAuth: true,
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
