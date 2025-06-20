import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { MainPage } from "@/pages/main";
import { NotFound } from "@/pages/not-found";
import "./styles/reset.scss";
import "./styles/variables.scss";
import "./styles/fonts.scss";
import "./styles/index.scss";
import { MainLayout } from "@features/layout/MainLayout";
import { FC } from "react";
import { LoginPage } from "@pages/auth/Login";
import { RegisterPage } from "@pages/auth/Register";
import { AuthLayout } from "@features/layout/AuthLayout";
import { FilmPage } from "@/pages/films";
import { AuthProvider } from "@/shared/lib/authProvider";
import { ProtectedRoute } from "@/shared/lib/ProtectedRoutes";
import { FilmDetail } from "@/pages/filmDetail";
import { Admin } from "@/pages/admin";
import { Profile } from "@/pages/profile";
import { UserFriends } from "@/pages/userFriends";
import { FriendProfile } from "@/pages/friendProfile";
import { useAuth } from "@/shared/lib/authProvider";
import { useNotificationHub } from "@/shared/lib/hooks/useNotificationHub";
import { useState } from "react";
import { ToastNotification } from "@/shared/components/ui/ToastNotification";

const NotificationListener = () => {
  const { token } = useAuth();
  const [toast, setToast] = useState<string | null>(null);
  let userId: string | null = null;
  try {
    userId = token ? JSON.parse(atob(token.split('.')[1])).nameid : null;
  } catch {}

  useNotificationHub(userId, (data) => {
    setToast("Вам пришла новая заявка в друзья");
  });

  return toast ? <ToastNotification message={toast} onClose={() => setToast(null)} /> : null;
};

const App: FC = () => {
  return (
    <AuthProvider>
      <Router>
        <NotificationListener />
        <Routes>
          <Route element={<MainLayout />}>
            <Route path="/" element={<MainPage />} />
            <Route path="films" element={<FilmPage />} />
            <Route path="films/:id" element={<FilmDetail />} />
            <Route path="profile/friends/:id" element={<FriendProfile/>}/>
          </Route>

          <Route path="/auth" element={<AuthLayout />}>
            <Route path="login" element={<LoginPage />} />
            <Route path="register" element={<RegisterPage />} />
          </Route>

          <Route element={<ProtectedRoute />}>
            <Route element={<MainLayout />}>
              <Route path="admin" element={<Admin />} />
              <Route path="profile" element={<Profile />} />
              <Route path="profile/friends" element={<UserFriends/>}></Route>
            </Route>
          </Route>

          <Route path="*" element={<NotFound />} />
        </Routes>
      </Router>
    </AuthProvider>
  );
};

export default App;
