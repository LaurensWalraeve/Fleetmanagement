import { useState, useEffect } from "react";
import { Input } from "@/app/components/ui/input";

export function Search() {
  const [apiStatus, setApiStatus] = useState<"success" | "error" | null>(null);

  useEffect(() => {
    // Simulating an API call
    fetch("http://localhost:54315/api/ping")
      .then((response) => {
        if (response.ok) {
          setApiStatus("success");
        } else {
          setApiStatus("error");
        }
      })
      .catch((error) => {
        setApiStatus("error");
      });
  }, []);

  return (
    <div className="flex items-center">
      <div
        className={`h-5 w-5 rounded-full mr-3 ${
          apiStatus === "success"
            ? "bg-green-500"
            : apiStatus === "error"
            ? "bg-red-500"
            : "bg-gray-500"
        }`}
        title={
          apiStatus === "success"
            ? "Connected"
            : apiStatus === "error"
            ? "Disconnected"
            : "Checking"
        }
      />
      <Input
        type="search"
        placeholder="Search..."
        className="ml-2 md:w-[100px] lg:w-[300px]"
      />
    </div>
  );
}
