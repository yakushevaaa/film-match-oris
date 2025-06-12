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

const App: FC = () => {
  return (
    <AuthProvider>
      <Router>
        <Routes>
          <Route element={<MainLayout />}>
            <Route path="/" element={<MainPage />} />
            <Route path="films" element={<FilmPage />} />
            <Route path="films/:id" element={<FilmDetail />} />
          </Route>

          <Route path="/auth" element={<AuthLayout />}>
            <Route path="login" element={<LoginPage />} />
            <Route path="register" element={<RegisterPage />} />
          </Route>

          <Route element={<ProtectedRoute />}>
            <Route element={<MainLayout />}></Route>
          </Route>

          <Route path="*" element={<NotFound />} />
        </Routes>
      </Router>
    </AuthProvider>
  );
};

export default App;
