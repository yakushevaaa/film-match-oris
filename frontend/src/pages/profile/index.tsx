import { FilmsList } from "@/features/FilmsList"
import { useEffect, useState } from "react";
import { axiosSettings } from "@shared/api/axiosSettings";
import { Film } from "@/entities/film";

export const Profile = () => {
    const [likedFilms, setLikedFilms] = useState<Film[]>([]);
    useEffect(() => {
        const fetchLikedFilms = async () => {
            try {
                const response = await axiosSettings.get<Film[]>("/Film/LikedBy/me");
                setLikedFilms(response.data);
            } catch (error) {
                console.error("Ошибка при загрузке лайкнутых фильмов:", error);
            }
        };
        fetchLikedFilms();
    }, []);
    return(
        <FilmsList films={likedFilms}/>
    )
}