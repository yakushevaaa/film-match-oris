import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "@/shared/lib/authProvider"; 
import { isTokenValid, getUserRole } from "@/shared/lib/tokenUtils";

export const ProtectedRoute = () => {
  const { token } = useAuth();

  if (!isTokenValid(token)) {
    return <Navigate to="/auth/login" replace />;
  }

  return <Outlet />;
};

export const AdminRoute = () => {
  const { token } = useAuth();

  if (!isTokenValid(token) || getUserRole(token) !== "admin") {
    return <Navigate to="/auth/login" replace />;
  }

  return <Outlet />;
};
