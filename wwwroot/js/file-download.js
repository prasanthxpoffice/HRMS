window.downloadFileFromStream = async (fileName, contentByte, contentType) => {
    const blob = new Blob([contentByte], { type: contentType || 'application/octet-stream' });
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}
