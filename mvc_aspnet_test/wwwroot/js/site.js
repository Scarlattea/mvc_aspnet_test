﻿$(function () {
    if ($("a.confirmDeletion").length) {
        $("a.confirmDeletion").click(() => {
            if (!confirm("confirm deletion")) return false;
        });
    }

    if ($("div.alert.notification").length) {
        setTimeout(() => {
            $("div.alert.notification").fadeOut();
        }, 2000);
    }
});
