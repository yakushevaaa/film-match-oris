import { Route, BrowserRouter as Router, Routes } from "react-router";
import { MainPage } from "@/pages/main";
import { NotFound } from "@/pages/not-found";
import "./styles/reset.scss";
import "./styles/variables.scss";
import "./styles/fonts.scss";
import { MainLayout } from "@features/layout/MainLayout";
import { FC } from "react";

const App: FC = () => {
  return (
    <Router>
      <Routes>
        <Route element={<MainLayout />}>
          <Route path="/" element={<MainPage />} />
        </Route>

        <Route path="*" element={<NotFound />} />
      </Routes>
    </Router>
  );
};

export default App;
