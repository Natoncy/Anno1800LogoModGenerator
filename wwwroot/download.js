window.downloadFileFromBase64 = (filename, base64) => {
    const link = document.createElement('a');
    link.download = filename;
    link.href = 'data:application/zip;base64,' + base64;
    link.click();
};