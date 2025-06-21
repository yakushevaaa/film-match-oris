import { useEffect, useState } from "react";
import {
  MaterialReactTable,
  type MRT_ColumnDef,
  type MRT_Row,
} from "material-react-table";
import { axiosSettings } from "@shared/api/axiosSettings";
import styles from "./index.module.scss";

interface Film {
  id: string;
  title: string;
  releaseDate: string;
  imageUrl?: string;
  shortDescription?: string;
}

export const BookmarkedFilmTable = () => {
  const [films, setFilms] = useState<Film[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    fetchBookmarkedFilms();
  }, []);

  const fetchBookmarkedFilms = async () => {
    setIsLoading(true);
    setError("");
    try {
      const res = await axiosSettings.get("/Film/Bookmarked");
      setFilms(res.data || []);
    } catch {
      setError("Не удалось загрузить закладки");
    } finally {
      setIsLoading(false);
    }
  };

  const handleRemoveBookmark = async (row: MRT_Row<Film>) => {
    try {
      await axiosSettings.post(`/Film/Bookmark/${row.original.id}`);
      setFilms((prev) => prev.filter((f) => f.id !== row.original.id));
    } catch {
      setError("Не удалось убрать из закладок");
    }
  };

  const columns: MRT_ColumnDef<Film>[] = [
    { accessorKey: "title", header: "Название", size: 200 },
    {
      accessorKey: "releaseDate",
      header: "Год",
      size: 100,
      Cell: ({ cell }) =>
        cell.getValue<string>()
          ? new Date(cell.getValue<string>()).getFullYear()
          : "",
    },
    { accessorKey: "shortDescription", header: "Описание", size: 300 },
    {
      header: "Действия",
      id: "actions",
      size: 120,
      Cell: ({ row }) => (
        <button
          className={styles.removeBookmarkButton}
          onClick={() => handleRemoveBookmark(row)}
        >
          Убрать из закладок
        </button>
      ),
    },
  ];

  return (
    <div style={{ padding: 20 }}>
      {error && <div style={{ color: "red", marginBottom: 8 }}>{error}</div>}
      <MaterialReactTable
        columns={columns}
        data={films}
        state={{ isLoading }}
        enableRowActions={false}
        enableColumnActions={false}
        enableSorting={false}
        enableFullScreenToggle={false}
        enableDensityToggle={false}
        enableHiding={false}
        enableFilters={false}
        enableGlobalFilter={false}
        enablePagination={true}
        muiTableBodyRowProps={{ hover: true }}
      />
    </div>
  );
};
