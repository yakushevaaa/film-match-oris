import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "@/shared/lib/authProvider"; 

export const ProtectedRoute = () => {
  const { token } = useAuth();

  if (!token) {
    return <Navigate to="/auth/login" replace />;
  }

  return <Outlet />;
};
