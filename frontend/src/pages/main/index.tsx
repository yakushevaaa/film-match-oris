import { Promo } from "@features/Promo";
import { RecommendCategories } from "@features/RecommendCategories";
import { CategoriesList } from "@features/CategoriesList";
import styles from "./index.module.scss";
import { InteractiveList } from "@/features/InteractiveList";

export const MainPage = () => {
  const categories = [
    {
      id: 1,
      name: "Боевики",
      imageUrl: "/images/category.webp",
      imageAlt: "Боевики",
    },
    {
      id: 2,
      name: "Комедии",
      imageUrl: "/images/category.webp",
      imageAlt: "Комедии",
    },
    {
      id: 3,
      name: "Драмы",
      imageUrl: "/images/category.webp",
      imageAlt: "Драмы",
    },
  ];

  return (
    <>
      <Promo />
      <InteractiveList />
      <RecommendCategories categories={categories} />
      <CategoriesList />
    </>
  );
};
